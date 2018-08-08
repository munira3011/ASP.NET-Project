function addEmployeeData(form){

    $.validator.unobtrusive.parse(form);
    if ($(form).valid) {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                $('#addView').html(response);
                $('ul.nav.nav-tabs a:eq(1)').tab('show');
            }

        });
        $(".myForm").reset();
    }
    
}
