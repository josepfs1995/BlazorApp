window.personas = {
  error: function () {
    alert("No se pudo guardar");
  },
  errorDelete: function () {
    alert("No se pudo eliminar");
  },
  success: function (id) {
    alert(`Se guardo correctamente ${id}`);
  },
  successDelete: function (id) {
    alert(`Se elimino correctamente ${id}`);
  }
};