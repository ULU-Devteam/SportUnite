export class User {

  constructor(public username: string,
              public password: string,
              public name: string,
              public age: number,
              public email: string,
              public city: string,
              public radius?: number) {}
}
