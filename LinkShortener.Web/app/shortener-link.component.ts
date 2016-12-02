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

	constructor(private linkService: LinkService) {
	}

	shorten(linkUrl: string) {
		this.linkService
			.addLink(linkUrl)
			.then(link => this.shortUrl = link);
	}
}
