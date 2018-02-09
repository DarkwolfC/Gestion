function ObtenerDatos() {
      
   var titulo = $("#titulo").val(); 
   var objetivo = $("#objetivo").val();
   var jefe = $("#jefeInmediato").val(); 
   var unidad = $("#idUnidad").val();
   var tipoPuesto = $("#idTipoPuesto").val();
   var fecha = $("#fechaCreacion").val();
   var activo = $("#activo").val();
   var aprobado = $("#aprobado ").val();

//funcion
   var funcion = $("#funcion").val();

//requisito
   var categoria = $("#idCategoria")
   var requisito = $("#requisito").val();

   //var result = titulo +"," + objetivo + ","+ jefe + ","+ unidad + ","+ tipoPuesto +","+ fecha + ","+ activo +"," + aprobado +"," + funcion +","+requisito
   // console.log(result)
}


function agregarFuncion() {
    $("#btnAgregar").click(function () {
        funcion = $("#funcion").val();
        var fila = "<tr><td>" + funcion + "</td><tr>";         
        console.log(fila);
        $("#tbody").append(fila);
      
    });
   
    
}