$(document).ready(function () {

    var input = '<label style="display: inline-block"><input class="form-control inputProject-js" type="text" name="foto[]" /> <a href="#" class="remove"> X</a></label>';
    $("input[name='add']").click(function (e) {
        $('#inputs_adicionais').append(input);
    });

    $('#inputs_adicionais').delegate('a', 'click', function (e) {
        e.preventDefault();
        $(this).parent('label').remove();
    });

});