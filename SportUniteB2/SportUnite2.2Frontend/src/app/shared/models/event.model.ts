import { Sport } from './sport.model';

export class Event {

  constructor(public eventId: number,
              public name: string,
              public peopleAmount: number,
              public archived: boolean,
              public sports: Sport[],
              public usernames: any[],
              public sportcomplex?: any,
              public date?: any,
              public long?: any,
              public lat?: any) {}

}
