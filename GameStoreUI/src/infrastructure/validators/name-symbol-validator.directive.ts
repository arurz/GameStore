import { Directive, forwardRef, HostListener, Input, OnInit } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, Validator, ValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';

const regexes: any = {
	bg: {
		default: /[^А-я- ]+$/,
		numbers: /[^А-Яа-я0-9- ]+$/,
		all: /[A-z]+$/
	},
	en: {
		default: /[^A-z- ]+$/,
		numbers: /[^A-Za-z0-9- ]+$/,
		all: /[А-я]+$/
	}
};

export function NameSymbolValidator(regexp: any): ValidatorFn {
	return (control: AbstractControl): { [key: string]: any } | null => {
		if (control.value) {
			if (!regexp) {
				return { 'Please, provide a valid language type': true };
			}

			if (regexp.test(control.value)) {
				return { 'Please, provide a valid name': true };
			}
		}

		return null;
	};
}

export function SetRegexp(acceptLanguage: string, withNumbers: boolean = false, allSymbols: boolean = false): Observable<any> {
	return new Observable(observer => {
		let regexp;
		if (acceptLanguage) {
			regexp = regexes[acceptLanguage];
		}
		else {
			regexp = regexes.bg;
		}

		if (withNumbers) {
			regexp = regexp.numbers;
		} else if (allSymbols) {
			regexp = regexp.all;
		} else {
			regexp = regexp.default;
		}

		observer.next(regexp);
		observer.complete();
	});
}

@Directive({
	selector: '[nameSymbolValidator]',
	providers: [
		{
			provide: NG_VALIDATORS,
			useExisting: forwardRef(() => NameSymbolValidatorDirective),
			multi: true
		}
	]
})
export class NameSymbolValidatorDirective implements Validator, OnInit {
	@Input() language: string;
	@Input() withNumbers: boolean;
	@Input() allSymbols: boolean;

	private valFn: ValidatorFn;
	localRegexp: any = null;

	constructor() { }

	@HostListener('keydown', ['$event'])
	keyDownEvent(event: KeyboardEvent) {
		if (!event.key)
			return;

		let filteredSymbol = this.filterName(event.key);
		if (event.key.length === 1 && (event.key !== filteredSymbol)) {
			event.preventDefault();
		}
	}

	ngOnInit() {
		SetRegexp(this.language, this.withNumbers, this.allSymbols)
			.subscribe(regexp => {
				this.localRegexp = regexp;
				this.valFn = NameSymbolValidator(regexp);
			})
	}

	validate(control: AbstractControl): { [key: string]: any } | null {
		return this.valFn(control);
	}

	filterName(nameSymbols: string) {
			return nameSymbols.replace(this.localRegexp, '');
	}
}
