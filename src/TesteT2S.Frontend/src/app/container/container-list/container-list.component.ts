import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { IApiPaginatedResource } from 'src/app/types/api';
import { Container } from '../Container';
import { ContainerService, IContainerServiceGetOptions } from '../container.service';

@Component({
  selector: 'app-container-list',
  templateUrl: './container-list.component.html',
  styleUrls: ['./container-list.component.css']
})
export class ContainerListComponent implements OnInit {

  containers$: Observable<IApiPaginatedResource<Container>>
  pageIndex = new BehaviorSubject<number>(1)
  containers: IApiPaginatedResource<Container>
  sortRule = "";
  sortProperty = "";

  constructor(private containerService: ContainerService) { }

  ngOnInit(): void {
    this.containers$ = this.pageIndex.pipe(switchMap(page => {
      let options: IContainerServiceGetOptions = { pagina: page }
      if (this.sortProperty) {
        options = { ...options, ordenarPor: `${this.sortProperty}_${this.sortRule}` }
      }
      return this.containerService.getAll(options);
    }));
    this.containers$.subscribe(data => console.log(data));
    this.containerService.getAll()
      .subscribe(data => this.containers = data);
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

}
