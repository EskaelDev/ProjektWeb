import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatDialog, MatPaginator, MatSort, MatTableDataSource} from '@angular/material';
import {merge, of as observableOf} from 'rxjs';
import {catchError, map, startWith, switchMap} from 'rxjs/operators';
import {MovieService} from '../_services/movie-service';
import {SelectionModel} from '@angular/cdk/collections';
import {DialogComponent} from '../dialog/dialog.component';
import {Router} from '@angular/router';
import {environment} from '../../environments/environment';
import {Movie} from '../_models/movie';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements AfterViewInit {
  displayedColumns: string[] = ['photo', 'title', 'description', 'edit', 'select'];
  dataSource: MatTableDataSource<Movie> = new MatTableDataSource([]);
  selection = new SelectionModel<Movie>(true, []);

  resultsLength = 0;
  isLoadingResults = false;

  env = environment;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private movieService: MovieService, private dialog: MatDialog, private router: Router) { }

  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.movieService.getAll(this.paginator.pageIndex, this.paginator.pageSize);
        }),
        map(movieApi => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.resultsLength = movieApi.count;
          return movieApi.movies;
        }),
        catchError(() => {
          this.isLoadingResults = false;
          return observableOf([]);
        })
      ).subscribe(data => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.sort = this.sort;
      });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: Movie): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${this.dataSource.filteredData.indexOf(row) + 1}`;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  noMovieSelected(): boolean {
    return this.selection.isEmpty();
  }

  onDeleteClicked() {
    // TODO this.openDialog('Are you sure to delete this movies?');
    this.selection.selected.forEach(item => {
      this.movieService.delete(item.id).subscribe(data => {
        const index: number = this.dataSource.data.findIndex(d => d === item);
        console.log(this.dataSource.data.findIndex(d => d === item));
        this.dataSource.data.splice(index, 1);
        this.dataSource = new MatTableDataSource<Movie>(this.dataSource.data);
        this.dataSource.sort = this.sort;
        this.resultsLength -= 1;
      });
    });
    this.selection.clear();
  }

  onEditClicked(row?: Movie) {
    this.router.navigate(['/movie', row.id]);
  }

  openDialog(errorMsg: string) {
    this.dialog.open(DialogComponent, {
      width: '30%',
      minHeight: '200px',
      data: { title: 'Attention', content: errorMsg, isDecisionDialog: true }
    });
  }
}
