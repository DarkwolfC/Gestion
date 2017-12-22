//var Enviar = function () {

    
//}

function Obtener() {
    var txtNp = jQuery('#npaso').val();
    var cbxR = jQuery('#cbxResponsable').val();
    var txta = jQuery('#descripcion').val();
    var cbxF = jQuery('#figura').val();
    var slctd = $('select#my_multi_select1').val();

    var dependences = [];
    $.each(slctd, function (key,value) {       
        dependences.push($('#'+value+'-selection > span').text());
    });


    var result = {
        'paso': txtNp,
        'responsable': cbxR,
        'descripcion': txta,
        'figura': cbxF,
        'dependencias':dependences
    };

 

} 