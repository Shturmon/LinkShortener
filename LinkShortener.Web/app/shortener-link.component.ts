import { Component } from "@angular/core";

import { Link } from "./link";
import { LinkService } from "./link.service";

@Component({
	selector: "shortener-link",
	templateUrl: "app/views/shortener-link.component.html"
})
export class ShortenerLinkComponent {
	linkUrl: string;
	shortUrl: string;
	validUrl: boolean;

	constructor(private linkService: LinkService) {
		this.validUrl = true;
	}

	onSubmit(linkUrl: string) {
		this.linkService
			.addLink(linkUrl)
			.then(link => {
				this.shortUrl = link;
				this.validUrl = true;
			})
			.catch(error => {
				var erroObject = error.json();
				if (erroObject.message) {
					this.validUrl = false;
				}
			});
	}
}
