import { NgModule, enableProdMode } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";
import { FormsModule }   from "@angular/forms";
import { RouterModule }   from "@angular/router";

import { AppComponent } from "./app.component";
import { ShortenerLinkComponent } from "./shortener-link.component";
import { LinkComponent } from "./link.component";

import { LinkService } from "./link.service";

enableProdMode();

@NgModule({
	imports: [ 
		BrowserModule,
		HttpModule,
		FormsModule,
		RouterModule.forRoot([
			{ path: "", component: ShortenerLinkComponent },
			{ path: "links", component: LinkComponent },
			{ path: "**", redirectTo: "/"}
		])
	],
	declarations: [ 
		AppComponent,
		ShortenerLinkComponent,
		LinkComponent
	],
	providers: [ 
		LinkService
	],
	bootstrap: [ AppComponent ]
})
export class AppModule { }