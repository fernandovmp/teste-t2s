import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HandlingRoutingModule } from './handling-routing.module';
import { HandlingListComponent } from './handling-list/handling-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [HandlingListComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NzTableModule,
    NzButtonModule,
    NzIconModule,
    NzFormModule,
    NzInputModule,
    NzSelectModule,
    HttpClientModule,
    HandlingRoutingModule
  ]
})
export class HandlingModule { }
