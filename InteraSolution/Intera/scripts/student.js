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

//COMANDO PARA BUSCAR A TABELA AO APERTAR ENTER
$(document).keypress('#studentsModal button[type=submit]', function (e) {
    if (e.which == 13 || e.keyCode == 13) {

        var texto = $('#studentsModal [name$=Search]').val();

        $.ajax({
            url: '/project/liststudent?nome=' + texto,
            method: 'Get',
            dataType: 'html',
            success: function (data) {
                $('#studentsModal table').replaceWith(data);
                $('#studentsModal input').val("").focus();
            },
        });
    }
});

//COMANDO PARA BUSCAR A TABELA AO CLICAR NO BOTÃO
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

//MODAL DE ATUALIZAR A SENHA -- FUNCIONANDO
$('#attsenha').on('shown.bs.modal', function () {
    $('#myInput').focus()
})

//TODA ESSA FUNCTION FOI TROCADA POR UM "ONKEYUP" NO CÓDIGO DA PAGE EDITPROFILE!
//$(document).ready(function () {
//    $('#newpassword').keyup(checarSenha());
//    $('#confirmpassword').keyup(checarSenha());
//})

//COMANDO PARA VERIFICAR SE AS SENHAS CONFEREM
function checarSenha() {
    var password = $("#newpassword").val();
    var confirmarPassword = $("#confirmpassword").val();
    if (password == '' || '' == confirmarPassword) {
        $("#divcheck").html("<span style='color:red'> Empty Field! </span>");
        document.getElementById("changepassword").disabled = true;
    } else if (password != confirmarPassword) {
        $("#divcheck").html("<span style='color:red'> Invalid Password! </span>");
        document.getElementById("changepassword").disabled = true;
    } else {
        $("#divcheck").html("<span style='color:green'> Valid Password! </span>");
        document.getElementById("changepassword").disabled = false;
    }
}

//MODAL DE CONFIRMAR EXCLUSÃO DE USUARIOS -- FUNCIONANDO
$('#confirmDelUser').on('shown.bs.modal', function () {
    $('#myInput').focus()
})

//MODAL DE CONFIRMAR EDIÇÕES -- FUNCIONANDO
$('#confirmEditProf').on('shown.bs.modal', function () {
    $('#myInput').focus()
})

//MODAL DE ADICIONAR NOVO USUÁRIO -- FUNCIONANDO
$('#createUser').on('shown.bs.modal', function () {
    $('#myInput').focus()
})