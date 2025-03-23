function setupCreateModal(httpType, url, modalHeaderText, extraActions) {
    $(document).on('click', '.create-button', function () {
        $.ajax({
            url: url,
            type: httpType,
            success: function (data) {
                $('.modal-body').html(data);
                $('#modal-header').text(modalHeaderText);

                extraActions();
            },
            error: function (data) {
                alert('Ошибка!');
                console.log(data);
            }
        });
    });
}

function setupFormSubmit(postEndpointUrl, successCallback) {
    console.log('Form submit is set up to POST: ' + postEndpointUrl);

    $(document).on('submit', '#modal-form', function (event) {
        event.preventDefault();

        $('#error-message').text('');

        $('#modal-form input').prop('disabled', false);

        let content = $('#modal-form').serialize();
        console.log('Form was submitted, executing POST: ' + postEndpointUrl);

        $.ajax({
            url: postEndpointUrl,
            type: 'POST',
            data: content,
            success: function (response) {
                if (response.isSuccessful) {
                    successCallback(response);
                }
                else {
                    console.log(response.errorMessage);
                    $('#error-message').text(response.errorMessage).show();
                    $('.modal-content').scrollTop(-10);
                }
            },
            error: function (response) {
                console.log(response.responseJSON.errorMessage);
                alert("Ошибка! Детали в консоли...");
            }
        });
    });
}

function DataTableParams(pageLength, columnDefs, language) {
    return {
        "pageLength": pageLength,
        "order": [],
        "columnDefs": columnDefs,
        "language": language,
    }
};

function getTableTexts() {
    var items = [
        "lengthMenuText",
        "zeroRecordText",
        "infoText",
        "infoEmptyText",
        "infoFilteredText",
        "emptyTableText",
        "loadingText",
        "processingText",
        "searchText",
        "firstText",
        "lastText",
        "nextText",
        "previousText",
        "ascText",
        "descText",
    ]

    var texts = [];
    for(let one of items){
        texts.push(
            document.getElementById(one).innerText
        );
        document.getElementById(one).remove();
    }
    return texts;
}