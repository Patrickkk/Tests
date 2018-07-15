export interface Pin {
    pinType: GpioPinGroup;
    pinNumber?: number;
    wiringPiPinNumber?: number;
    bcmPinNumber?: number;
    headerPinNumber?: number;
    header?: number;
    name?: string;
    capabilities?: number[];
    pinMode?: number;
    inputPullMode?: number;
    pwmRegister?: number;
    pwmMode?: number;
    pwmRange?: number;
    pwmClockDivisor?: number;
    isInSoftPwmMode?: boolean;
    softPwmValue?: number;
    softPwmRange?: number;
    isInSoftToneMode?: boolean;
    softToneFrequency?: number;
    interruptEdgeDetection?: number;
    interruptCallback?: any;
}

export interface PhysicalPin extends Pin {
    physicalPinNumber: number;
}

export enum GpioPinGroup {
    default,
    gpio,
    reserved,
    threeVolt,
    fiveVolt,
    ground,
    spi,
    i2c,
}
