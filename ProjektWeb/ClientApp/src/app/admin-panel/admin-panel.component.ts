import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatDialog, MatPaginator, MatSort, MatTableDataSource} from '@angular/material';
import {merge, of as observableOf} from 'rxjs';
import {catchError, map, startWith, switchMap} from 'rxjs/operators';
import {Movie} from '../_models/movie';
import {MovieService} from '../_services/movie-service';
import {SelectionModel} from '@angular/cdk/collections';
import {DialogComponent} from '../dialog/dialog.component';
import {UserService} from '../_services/user-service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements AfterViewInit  {
  displayedColumns: string[] = ['photo', 'title', 'description', 'edit', 'select'];
  movies: Movie[] = [{ title: 'Suits', description: 'On the run from a drug deal gone bad, brilliant college dropout Mike Ross, finds himself working with Harvey Specter, one of New York City\'s best lawyers.',
    imagePath: 'https://www.multikurs.pl/uploads_public/cms/component-25162/suits-7591.jpg', tags: ['lawyers', 'comedy', 'suits']},
    { title: 'Suits2', description: 'On the run from a drug deal gone bad, brilliant college dropout Mike Ross, finds himself working with Harvey Specter, one of New York City\'s best lawyers.',
      imagePath: 'https://www.multikurs.pl/uploads_public/cms/component-25162/suits-7591.jpg', tags: ['lawyers', 'comedy', 'suits']}];

  dataSource: MatTableDataSource<Movie> = new MatTableDataSource([]);
  selection = new SelectionModel<Movie>(true, []);

  isLoadingResults = false;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private movieService: MovieService, private dialog: MatDialog) {}

  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    for (let _i = 0; _i < 9; _i++) {
      const old = this.movies[0];
      const newMovie: Movie = {
        title: old.title,
        description: old.description,
        imagePath: old.imagePath,
        tags: []
      };
      this.movies.push(newMovie);
    }

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          // return this.movieService.getAll(100);
          return observableOf(this.movies);
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;

          return data;
        }),
        catchError(() => {
          this.isLoadingResults = false;
          return observableOf([]);
        })
      ).subscribe(data => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
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
    this.openDialog('Are you sure to delete this movies?');
    // TODO remove elements
    this.selection.clear();
  }

  onEditClicked(row?: Movie) {
  }

  openDialog(errorMsg: string) {
    this.dialog.open(DialogComponent, {
      width: '30%',
      minHeight: '200px',
      data: {title: 'Attention', content: errorMsg, isDecisionDialog: true}
    });
  }
}
