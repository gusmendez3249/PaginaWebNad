import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PaginaInfoRoutingModule } from './pagina-info-routing.module';
import { PaginaInfoComponent } from './pagina-info/pagina-info.component';


@NgModule({
  declarations: [
    PaginaInfoComponent
  ],
  imports: [
    CommonModule,
    PaginaInfoRoutingModule
  ]
})
export class PaginaInfoModule { }
