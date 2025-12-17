import React, { type JSX } from "react";
import { cn } from "../../utils/styleutil";
import './input.scss';


interface InputTextElementProps extends React.InputHTMLAttributes<HTMLInputElement> {
    withErro: boolean;
    errorMessage?: string;
    label?: string;
    additionalClassname?: string;
    disabled?: boolean;
    change?: (event: React.ChangeEvent<HTMLInputElement>) => void;
    value?: string;
}

function InputTextElement(props: InputTextElementProps): JSX.Element {
    
    function changeEvent(event: React.ChangeEvent<HTMLInputElement>) {
        if (props.change) {
            props.change(event);
        }
    }

    return (
        <div className={cn("space-y-2 text-left", props.additionalClassname)}>
            {props.label != undefined && props.label != null && props.label.length > 0 &&
                <label className={cn("text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70")} htmlFor={props.id}>
                    {props.label}
                </label>
            }
            <input id={props.id} name={props.id} value={props.value} onChange={(event) =>changeEvent(event)} placeholder={props.placeholder} disabled={props.disabled} type={props.type ? props.type : 'text'} className={cn("flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-base ring-offset-background file:border-0 file:bg-transparent file:text-sm file:font-medium file:text-foreground placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50 md:text-sm", props.withErro ? 'errorBorder':'')} />
            {props.withErro && props.errorMessage &&
                <p className="text-sm text-destructive errortext">{props.errorMessage}</p>
            }
        </div>
    )
}

export { InputTextElement };