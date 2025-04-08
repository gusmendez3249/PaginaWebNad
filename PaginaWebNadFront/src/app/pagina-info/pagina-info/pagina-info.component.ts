import { Component } from '@angular/core';

@Component({
  selector: 'app-pagina-info',
  templateUrl: './pagina-info.component.html',
  styleUrls: ['./pagina-info.component.css']
})
export class PaginaInfoComponent {
  clientes = ['Cliente 1', 'Cliente 2', 'Cliente 3'];
  servidores = ['Servidor 1', 'Servidor 2', 'Servidor 3'];
  sistemas = ['Sistema A', 'Sistema B', 'Sistema C'];
  filtros = {
    cliente: '',
    servidor: '',
    sistema: ''
  };

  // Datos simulados para la tabla
 // Datos simulados para la tabla
tablaDatos = [
  {
    cliente: 'Cliente 1',
    servidorApp: 'Servidor 1',
    servidorBd: 'DB 1',
    sox: 'Sí',
    sgce: 'No',
    sgro: 'Sí',
    sgov: 'Sí',
    sgi: 'No',
    sgc: 'Sí',
    sia: 'No',
    sgov02: 'Sí',
    sscs: 'No',
    sgovvery: 'Sí',
    sgfo: 'No',
    mjeVlegal: 'Sí',
    gtospes: 'No',
    arco: 'Sí',
    proformas: 'No',
    sga: 'Sí'
  },
  {
    cliente: 'Cliente 2',
    servidorApp: 'Servidor 2',
    servidorBd: 'DB 2',
    sox: 'No',
    sgce: 'Sí',
    sgro: 'No',
    sgov: 'No',
    sgi: 'Sí',
    sgc: 'No',
    sia: 'Sí',
    sgov02: 'No',
    sscs: 'Sí',
    sgovvery: 'No',
    sgfo: 'Sí',
    mjeVlegal: 'No',
    gtospes: 'Sí',
    arco: 'No',
    proformas: 'Sí',
    sga: 'No'
  },
  // Agregar más datos según sea necesario
];


  // Métodos para manejar los filtros
  limpiarFiltros() {
    this.filtros = {
      cliente: '',
      servidor: '',
      sistema: ''
    };
  }

  filtrarDatos() {
    // Aquí podrías agregar la lógica para filtrar según los filtros seleccionados
    console.log('Filtrando datos...');
  }

  generarReporte() {
    // Simulación de generación de reporte (sin funcionalidad real)
    console.log('Generando reporte...');
  }
}
