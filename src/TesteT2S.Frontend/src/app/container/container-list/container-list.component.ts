import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { IApiPaginatedResource } from 'src/app/types/api';
import { Container } from '../Container';
import { ContainerService } from '../container.service';

@Component({
  selector: 'app-container-list',
  templateUrl: './container-list.component.html',
  styleUrls: ['./container-list.component.css']
})
export class ContainerListComponent implements OnInit {

  containers$: Observable<IApiPaginatedResource<Container>>

  constructor(private containerService: ContainerService) { }

  ngOnInit(): void {
    this.containers$ = this.containerService.getAll();
  }

  changePage(pageIndex: number) {
    // this.containers$ = this.containerService.getAll({
    //   pagina: pageIndex
    // });
  }

}
