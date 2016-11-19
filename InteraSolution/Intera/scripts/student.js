//MODAL DO ADD STUDENT NO PROJETO - ATUALIZADO
$('#stundetsModal').on('shown.bs.modal', function () {
    $('#myInput').focus();
})

//COMANDO PARA LIMPAR A TABLE DO MODAL -- FUNCIONANDO
$('#btnAddStudent').on('click', function () {
    $('#myInput').focus();
    $('#studentsModal table').children().remove();
})

//COMANDO PARA PUXAR A TABELA e LIMPAR BOTÃO SEARCH -- FUNCIONANDO
$(document).on('click', '#studentsModal button[type=submit]', function () {

    var texto = $('#studentsModal [name$=Search]').val();

    $.ajax({
        url: '/project/liststudent?nome=' + texto,
        method: 'Get',
        dataType: 'html',
        success: function (data) {
            $('#studentsModal table').replaceWith(data);
            //$('#studentsModal button[type=submit]').val("").focus();
            $('#studentsModal input').val("").focus();
        },
        error: function () {
            alert('error');
        }
    });
});