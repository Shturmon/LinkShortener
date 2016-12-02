"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
require("rxjs/add/operator/toPromise");
var LinkService = (function () {
    function LinkService(http) {
        this.http = http;
        this.rootApiUrl = "api";
        this.headers = new http_1.Headers({ 'Content-Type': "application/json" });
    }
    LinkService.prototype.getLinks = function () {
        return this.http.get(this.rootApiUrl + "/links")
            .toPromise()
            .then(function (res) { return res.json(); })
            .catch(this.handleError);
    };
    LinkService.prototype.addLink = function (linkUrl) {
        var url = this.rootApiUrl + "/short-link/?url=" + linkUrl;
        return this.http.get(url, { headers: this.headers })
            .toPromise()
            .then(function (res) { return res.json(); })
            .catch(this.handleError);
    };
    LinkService.prototype.handleError = function (error) {
        return Promise.reject(error.message || error);
    };
    LinkService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], LinkService);
    return LinkService;
}());
exports.LinkService = LinkService;
//# sourceMappingURL=link.service.js.map