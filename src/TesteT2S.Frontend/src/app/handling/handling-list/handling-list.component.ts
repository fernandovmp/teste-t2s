import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, ValidationErrors } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { IApiGetOptions, IApiPaginatedResource } from 'src/app/types/api';
import { Handling } from '../Handling';
import { HandlingService } from '../handling.service';

@Component({
  selector: 'app-handling-list',
  templateUrl: './handling-list.component.html',
  styleUrls: ['./handling-list.component.css']
})
export class HandlingListComponent implements OnInit {

  handlings$: Observable<IApiPaginatedResource<Handling>>
  pageIndex = new BehaviorSubject<number>(-1)
  sortRule = "";
  sortProperty = "";
  containerNumber = new FormControl('', [this.containerNumberValidator]);

  constructor(private router: Router, private handlingService: HandlingService) { }

  ngOnInit(): void {
    this.handlings$ = this.pageIndex.pipe(switchMap(this.fetchHandlings(this.handlingService)));
  }

  onSearch() {
    this.pageIndex.next(1);
  }

  fetchHandlings(service: HandlingService) {
    return (page: number) => {
      if (page === -1) {
        return of({
          pagina: 1,
          paginaAtual: 1,
          paginasTotais: 1,
          tamanho: 10,
          quantidadeTotal: 0,
          dados: []
        });
      }
      let options: IApiGetOptions = { pagina: page }
      if (this.sortProperty) {
        options = { ...options, ordenarPor: `${this.sortProperty}_${this.sortRule}` }
      }
      return service.getAll(this.containerNumber.value, options)
        .pipe(catchError(this.handleError()));
    }
  }

  handleError() {
    return (error: HttpErrorResponse) => {
      if (error.status === 404) {
        this.containerNumber.setErrors({ 'notfound': true });
        this.pageIndex.next(-1);
      }
      return of({
        pagina: 1,
        paginaAtual: 1,
        paginasTotais: 1,
        tamanho: 10,
        quantidadeTotal: 0,
        dados: []
      });
    }
  }

  changePage(pg: number) {
    this.pageIndex.next(pg);
  }

  sort(property: string, rule: "ascend" | "descend" | null) {
    this.sortProperty = property;
    if (rule === "ascend") {
      this.sortRule = "asc";
    }
    else if (rule === "descend") {
      this.sortRule = "desc";
    }
    else {
      this.sortRule = null;
      this.sortProperty = null;
    }

    this.pageIndex.next(this.pageIndex.value);
  }

  goToNew() {
    this.router.navigate(['/movimentacoes/container', this.containerNumber.value, 'novo']);
  }

  containerNumberValidator(control: AbstractControl): ValidationErrors {
    const value = String(control.value);
    if (value.length != 11) {
      return {
        'containerNumberValidation': true
      };
    }
    if (!/^[a-zA-Z]+$/.test(value.substring(0, 4))) {
      return {
        'containerNumberValidation': true
      };
    }
    if (!/^[0-9]+$/.test(value.substring(4))) {
      return {
        'containerNumberValidation': true
      };
    }
    return null;
  }
}
