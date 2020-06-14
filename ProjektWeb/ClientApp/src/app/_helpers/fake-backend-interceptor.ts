import { HTTP_INTERCEPTORS, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { delay, dematerialize, materialize, mergeMap } from 'rxjs/operators';
import { User } from '../_models/User';
import { Role } from '../_models/role';
import { Movie } from '../_models/movie';

const users: User[] = [{ email: 'test@gmail.com', password: 'test', name: 'Test', role: Role.Admin }];
const movies: Movie[] = [{
    id: 1, title: 'Suits', description: 'On the run from a drug deal gone bad, brilliant college dropout Mike Ross, finds himself working with Harvey Specter, one of New York City\'s best lawyers.',
    imagePath: 'https://www.multikurs.pl/uploads_public/cms/component-25162/suits-7591.jpg', tags: ['lawyers', 'comedy', 'suits']
}];

@Injectable()
export class FakeBackendInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const { url, method, headers, body } = request;

        // wrap in delayed observable to simulate server api call
        return of(null)
            .pipe(mergeMap(handleRoute))
            .pipe(materialize()) // call materialize and dematerialize to ensure delay even if an error
            // is thrown (https://github.com/Reactive-Extensions/RxJS/issues/648)
            .pipe(delay(500))
            .pipe(dematerialize());

        function handleRoute() {
            switch (true) {
                case url.endsWith('/users/authenticate') && method === 'POST':
                    return authenticate();
                case url.endsWith('/users') && method === 'GET':
                    return getUsers();
                case url.endsWith('/users') && method === 'POST':
                    return createUser();
                case url.endsWith('/movies') && method === 'GET':
                    return getMovies();
                default:
                    // pass through any requests not handled above
                    return next.handle(request);
            }
        }

        // route functions

        function authenticate() {
            const { email, password } = body;
            const user = users.find(x => x.email === email && x.password === password);
            if (!user) { return error('Username or password is incorrect'); }
            return ok({
                name: user.name,
                token: 'fake-jwt-token',
                role: user.role
            });
        }

        function getMovies() {
            if (!isLoggedIn()) { return unauthorized(); }
            return ok(movies);
        }

        function getUsers() {
            if (!isLoggedIn()) { return unauthorized(); }
            return ok(users);
        }

        function createUser() {
            const user: User = body;
            const isAlreadyHere = users.find(u => u.email === user.email);
            if (isAlreadyHere) { return error('User with this email already exist'); }
            user.role = Role.User;
            users.push(user);
            return ok(user);
        }

        // helper functions

        function ok(body?) {
            return of(new HttpResponse({ status: 200, body }));
        }

        function error(message) {
            return throwError({ error: { message } });
        }

        function unauthorized() {
            return throwError({ status: 401, error: { message: 'Unauthorised' } });
        }

        function isLoggedIn() {
            return headers.get('Authorization') === 'Bearer fake-jwt-token';
        }
    }
}

export let fakeBackendProvider = {
    // use fake backend in place of Http service for backend-less development
    provide: HTTP_INTERCEPTORS,
    useClass: FakeBackendInterceptor,
    multi: true
};

