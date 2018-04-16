export class UrlConfig {
  get nodeUrl(): string {
    return this._nodeUrl;
  }

  set nodeUrl(value: string) {
    this._nodeUrl = value;
  }
  get sqlUrl(): string {
    return this._sqlUrl;
  }

  set sqlUrl(value: string) {
    this._sqlUrl = value;
  }
  private _nodeUrl: string;
  private _sqlUrl: string;
  private local: boolean = false;

  constructor() {

      if(this.local){
        this._nodeUrl = 'http://localhost:3000/api/';
        this._sqlUrl = 'https://sportuniteapi.azurewebsites.net/';
      }else{
        this._nodeUrl = 'https://sportunitenodeapi.azurewebsites.net/api/';
        this._sqlUrl = 'https://sportuniteapi.azurewebsites.net/';
      }
  }
}

