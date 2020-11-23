import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { HandlingService } from '../handling.service';

@Component({
  selector: 'app-handling-form',
  templateUrl: './handling-form.component.html',
  styleUrls: ['./handling-form.component.css']
})
export class HandlingFormComponent implements OnInit {

  handlingForm = new FormGroup({
    numero: new FormControl(''),
    navio: new FormControl('', [
      Validators.required,
      Validators.minLength(1),
      Validators.maxLength(50)
    ]),
    tipoMovimentacao: new FormControl("0"),
    dataInicio: new FormControl('', [
      Validators.required
    ]),
    dataFim: new FormControl('', [
      Validators.required
    ])
  });
  dataInicio = new Date();
  dataFim = new Date();
  isInEditMode$: Observable<boolean>;
  handlingId = 0;

  constructor(private router: Router, private route: ActivatedRoute, private handlingService: HandlingService) { }

  ngOnInit(): void {
    this.isInEditMode$ = this.route.data.pipe(
      switchMap(data => {
        const { isInEditMode } = data;
        this.handlingForm.get('numero').disable();
        this.route.paramMap.subscribe(params => {
          const container = params.get('containerNumber');
          this.handlingForm.get('numero').setValue(container);
          if (isInEditMode) {
            this.handlingId = +params.get('id');
            this.handlingService.getById(container, this.handlingId).subscribe(data => {
              this.handlingForm.get('navio').setValue(data.navio);
              this.handlingForm.get('tipoMovimentacao').setValue(String(data.tipoMovimentacao.id));
              this.handlingForm.get('dataInicio').setValue(String(data.dataInicio));
              this.handlingForm.get('dataFim').setValue(String(data.dataFim));
            });
          }
        });
        return of(isInEditMode !== undefined && isInEditMode);
      })
    );
  }

  onSubmit() {
    let isInEditMode = false;
    this.isInEditMode$.subscribe(value => isInEditMode = value);
    const container = this.handlingForm.get('numero').value;
    const handlingData = {
      numero: String(this.handlingForm.get('numero').value),
      navio: String(this.handlingForm.get('navio').value),
      tipoMovimentacao: Number(this.handlingForm.get('tipoMovimentacao').value),
      dataInicio: new Date(this.handlingForm.get('dataInicio').value),
      dataFim: new Date(this.handlingForm.get('dataFim').value)
    };
    if (isInEditMode) {
      this.handlingService.update(container, this.handlingId, handlingData)
        .pipe(catchError(this.handleError()))
        .subscribe(() => this.router.navigate(['/movimentacoes']));
    } else {
      this.handlingService.post(container, handlingData)
        .pipe(catchError(this.handleError()))
        .subscribe(() => this.router.navigate(['/movimentacoes']));
    }
  }

  handleError() {
    return (error: HttpErrorResponse) => {
      return throwError('Algo de errado aconteceu, tente novamente');
    }
  }

  onDelete() {
    const container = this.handlingForm.get('numero').value;
    this.handlingService.delete(container, this.handlingId)
      .pipe(catchError(this.handleError()))
      .subscribe(() => this.router.navigate(['/movimentacoes']));
  }

}
