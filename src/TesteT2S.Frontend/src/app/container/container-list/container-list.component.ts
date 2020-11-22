import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
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
  pageIndex = new BehaviorSubject<number>(1)
  containers: IApiPaginatedResource<Container>

  constructor(private containerService: ContainerService) { }

  ngOnInit(): void {
    this.containers$ = this.pageIndex.pipe(switchMap(page => {
      console.log(page);
      return this.containerService.getAll({ pagina: page });
    }));
    this.containers$.subscribe(data => console.log(data));
    this.containerService.getAll()
      .subscribe(data => this.containers = data);
  }

  changePage(pg: number) {
    this.pageIndex.next(pg);
  }

}
