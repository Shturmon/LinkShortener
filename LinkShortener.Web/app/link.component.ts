import { Component, OnInit } from "@angular/core";

import { Link } from "./link";
import { LinkService } from "./link.service";

@Component({
  selector: "links",
  templateUrl: "app/views/link.component.html"
})
export class LinkComponent implements OnInit {
	links: Link[];

	constructor(private linkService: LinkService) {
	}

	ngOnInit(): void {
		this.getLinks();
	}

	getLinks(): void {
		this.linkService
			.getLinks()
			.then(links => this.links = links);
	}
}
