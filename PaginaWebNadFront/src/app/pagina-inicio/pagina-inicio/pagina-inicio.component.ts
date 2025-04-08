import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pagina-inicio',
  templateUrl: './pagina-inicio.component.html',
  styleUrls: ['./pagina-inicio.component.css']
})
export class PaginaInicioComponent {
  cliente: string = '';
  usuario: string = '';
  
  constructor(private router: Router) {}

  buscar() {
    console.log('Buscando con:', this.cliente, this.usuario);
    // Lógica de búsqueda
  }

  cancelar() {
    this.cliente = '';
    this.usuario = '';
  }
   // Método para redirigir a la misma pantalla sin condiciones
   irASiguientePantalla() {
    this.router.navigateByUrl('/info');  // Aquí simplemente rediriges a la misma URL
  }
}
