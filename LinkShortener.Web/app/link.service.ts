import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";

import "rxjs/add/operator/toPromise";

import { Link } from "./link";

@Injectable()
export class LinkService {
	private rootApiUrl = "api";
	private headers = new Headers({'Content-Type': "application/json"});

	constructor(private http: Http) { }

	getLinks(): Promise<Link[]> {
		return this.http.get(`${this.rootApiUrl}/links`)
               .toPromise()
               .then(res => res.json() as Link[])
               .catch(this.handleError);
	}

	addLink(linkUrl: string): Promise<string> {
		const url = this.rootApiUrl + "/short-link/?url=" + linkUrl;
		return this.http.get(url, {headers: this.headers})
			.toPromise()
			.then(res => res.json())
			.catch(this.handleError);
	}

	private handleError(error: any): Promise<any> {
		return Promise.reject(error.message || error);
	}
}