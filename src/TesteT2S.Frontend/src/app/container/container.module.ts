import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NzTableModule } from 'ng-zorro-antd/table';

import { ContainerRoutingModule } from './container-routing.module';
import { ContainerListComponent } from './container-list/container-list.component';
import { HttpClientModule } from '@angular/common/http';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';


@NgModule({
  declarations: [ContainerListComponent],
  imports: [
    CommonModule,
    NzTableModule,
    NzButtonModule,
    NzIconModule,
    HttpClientModule,
    ContainerRoutingModule
  ]
})
export class ContainerModule { }
