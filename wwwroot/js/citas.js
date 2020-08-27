window.citas = {
    cargando: function(){
        $("#modalCargando").modal("show");
    },
    nocargando:function(){
        $("#modalCargando").modal("hide");
    },
    escribirConsola: function(msg) { console.log(msg)}
};