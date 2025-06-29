﻿using ClosedXML.Excel;
using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using EmisTracking.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
    [Authorize(Roles = $"{Services.Constants.OperatorRole},{Services.Constants.AdminRole}")]
    public class EmissionSourcesController : BaseDropdownViewController<EmissionSourceViewModel>
    {
        private readonly IBaseApiService<SubdivisionViewModel> _subdivisionService;
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;
        private readonly IModeApiService _modeService;
        private readonly ISourceSubstanceApiService _sourceSubstanceApiService;

        public EmissionSourcesController(
            IBaseApiService<EmissionSourceViewModel> emissionSourceService,
            IBaseApiService<SubdivisionViewModel> subdivisionService,
            IBaseApiService<MethodologyViewModel> methodologyService,
            IBaseApiService<PollutantViewModel> pollutantService,
            IModeApiService modeService,
            ISourceSubstanceApiService sourceSubstanceApiService)
        {
            _apiService = emissionSourceService;
            _apiService = emissionSourceService;
            _subdivisionService = subdivisionService;
            _methodologyService = methodologyService;
            _modeService = modeService;
            _sourceSubstanceApiService = sourceSubstanceApiService;
        }

        protected override string CreationTitle => LangResources.Titles.EmissionSourcesCreate;
        protected override string UpdateTitle => LangResources.Titles.EmissionSourcesUpdate;


        [Authorize(Roles = Services.Constants.AdminRole)]
        [LoadLayoutDataFilter]
        [HttpGet("createForSubdivision/{id}")]
        public async Task<IActionResult> CreateForSubdivision([FromRoute] string id)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new EmissionSourceViewModel();
            await LoadDropdownsValuesAsync(model);
            model.SubdivisionId = model.Subdivisions.Any(s => s.Value == id) ? id : null;

            return View(Constants.FormView, model);
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("{id}")]
        public override async Task<IActionResult> Item([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.GetByIdAsync(id, loadDependencies: true);

            if (response.Success)
            {
                var sourceSubstancesResponse = await _sourceSubstanceApiService.GetAllByEmissionSourceIdAsync(
                    response.Data.Id, loadDependencies: true);

                var model = new ModelWithDependencies<EmissionSourceViewModel, SourceSubstanceViewModel>
                {
                    MainItem = response.Data,
                    Dependencies = sourceSubstancesResponse.Success ?
                        sourceSubstancesResponse.Data
                        : Enumerable.Empty<SourceSubstanceViewModel>(),
                };

                return View(model);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [HttpPost("create")]
        public override async Task<IActionResult> Create([FromForm] EmissionSourceViewModel model)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            if ((model.MethodologyId == null && model.ModeId == null)
                || model.MethodologyId != null && model.ModeId != null)
            {
                ModelState.AddModelError(string.Empty, LangResources.EitherMethodologyAndModeMustBeSet);
            }

            if (!ModelState.IsValid)
            {
                await LoadDropdownsValuesAsync(model);

                return View(Constants.FormView, model);
            }

            if(model.ModeId != null)
            {
                await UpdateModeMethodologiesAsync(model);
            }

            var response = await _apiService.CreateAsync(model);
            if (response.Success)
            {
                return RedirectToAction(nameof(Item), new { id = response.Data });
            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                await LoadDropdownsValuesAsync(model);

                return View(Constants.FormView, model);
            }
        }

        private async Task UpdateModeMethodologiesAsync(EmissionSourceViewModel model)
        {
            var methodologiesResponse = await _methodologyService.GetByIdAsync(model.MethodologyId);
            var modeResponse = await _modeService.GetByIdAsync(model.ModeId);

            if (methodologiesResponse.Success && model.ModeId != null)
            {
                model.MethodologyId = methodologiesResponse.Data.Id;
            }
            else
            {
                // FIXME Log error
            }

            if (modeResponse.Success && model.ModeId != null)
            {
                model.ProcessCategory = modeResponse.Data.ProcessCategory;
            }
            else
            {
                // FIXME Log error
            }
        }

        [HttpPost("update/{id}")]
        public override async Task<IActionResult> Update([FromRoute] string id, [FromForm] EmissionSourceViewModel model)
        {
            ViewData[AspAction] = nameof(Update);
            ViewData[Title] = UpdateTitle;

            if (!ModelState.IsValid)
            {
                return View(Constants.FormView, model);
            }

            var response = await _apiService.UpdateAsync(model);

            if (response.Success)
            {
                return RedirectToAction(nameof(Item), new { id });
            }
            else
            {
                await LoadDropdownsValuesAsync(model);

                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View("Form", model);
            }
        }

        [HttpGet("excel/{id}")]
        public async Task<IActionResult> ExportToExcel([FromRoute] string id)
        {
            var baseDirectory = AppContext.BaseDirectory;
            var projectDirectory = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", ".."));

            var filePath = Path.Combine(projectDirectory, Constants.ExcelTemplate);

            using var workbook = new XLWorkbook(filePath);

            var stream = new MemoryStream();
            workbook.SaveAs(stream);

            if (stream.Length == 0)
            {
                throw new Exception("Failed to write Excel workbook to stream.");
            }

            stream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "Выбросы.xlsx"
            };
        }

        public override async Task LoadDropdownsValuesAsync(EmissionSourceViewModel model)
        {
            var subdivisionsResponse = await _subdivisionService.GetAllAsync();
            var methodologiesResponse = await _methodologyService.GetAllAsync();
            var modesResponse = await _modeService.GetAllWithMethodologiesAsync();

            if (subdivisionsResponse.Success && methodologiesResponse.Success && modesResponse.Success)
            {
                model.Subdivisions = subdivisionsResponse.Data
                    .Select(s => new DropdownItemModel { Value = s.Id, Name = s.Name })
                    .ToList();

                model.Methodologies = methodologiesResponse.Data
                    .Select(m => new DropdownItemModel { Value = m.Id, Name = m.Name })
                    .ToList();

                model.Modes = modesResponse.Data
                    .Select(m => new DropdownItemModel { Value = m.Id, Name = m.Name })
                    .ToList();
            }
        }
    }
}