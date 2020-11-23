import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { map, switchMap, catchError } from 'rxjs/operators';
import { ContainerService } from '../container.service';

@Component({
  selector: 'app-container-form',
  templateUrl: './container-form.component.html',
  styleUrls: ['./container-form.component.css']
})
export class ContainerFormComponent implements OnInit {
  containerForm = new FormGroup({
    numero: new FormControl('', [
      Validators.required,
      this.containerNumberValidator
    ]),
    cliente: new FormControl('', [
      Validators.required,
      Validators.minLength(1),
      Validators.maxLength(50)
    ]),
    tipo: new FormControl("20"),
    categoria: new FormControl("0"),
    status: new FormControl("0")
  });
  isInEditMode$: Observable<boolean>;
  containerId = 0;

  constructor(private router: Router, private route: ActivatedRoute, private containerService: ContainerService) { }

  ngOnInit(): void {
    this.isInEditMode$ = this.route.data.pipe(
      switchMap(data => {
        const { isInEditMode } = data;
        if (isInEditMode) {
          this.containerForm.get('numero').disable();
          this.route.paramMap.subscribe(params => {
            this.containerId = +params.get('id');
            this.containerService.getById(this.containerId).subscribe(data => {
              this.containerForm.get('numero').setValue(data.numero);
              this.containerForm.get('cliente').setValue(data.cliente);
              this.containerForm.get('tipo').setValue(String(data.tipo));
              this.containerForm.get('status').setValue(String(data.status.id));
              this.containerForm.get('categoria').setValue(String(data.categoria.id));
            });
          });
        }
        return of(isInEditMode !== undefined && isInEditMode);
      })
    );
  }

  onSubmit() {
    let isInEditMode = false;
    this.isInEditMode$.subscribe(value => isInEditMode = value);

    const containerData = {
      numero: String(this.containerForm.get('numero').value),
      cliente: String(this.containerForm.get('cliente').value),
      tipo: Number(this.containerForm.get('tipo').value),
      status: Number(this.containerForm.get('status').value),
      categoria: Number(this.containerForm.get('categoria').value)
    };
    if (isInEditMode) {
      this.containerService.update(this.containerId, containerData)
        .pipe(catchError(this.handleError()))
        .subscribe(() => this.router.navigate(['/containers']));
    } else {
      this.containerService.post(containerData)
        .pipe(catchError(this.handleError()))
        .subscribe(() => this.router.navigate(['/containers']));
    }
  }

  handleError() {
    return (error: HttpErrorResponse) => {
      if (error.status === 409) {
        this.containerForm.get('numero').setErrors({ conflict: true })
      }
      return throwError('Algo de errado aconteceu, tente novamente');
    }
  }

  onDelete() {
    this.containerService.delete(this.containerId)
      .pipe(catchError(this.handleError()))
      .subscribe(() => this.router.navigate(['/containers']));
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

  goToHandlings() {
    this.router.navigate(['/movimentacoes/container', this.containerForm.get('numero').value, 'novo']);
  }

}
