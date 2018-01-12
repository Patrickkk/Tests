import { Input } from "@angular/core";

export abstract class GenericFieldEditorBase {
    @Input() fieldSettings: IFieldSetting[];

    constructor() {
    }
}

export interface IFieldSetting {
    settingName: string;
    settingValue: string;
}
