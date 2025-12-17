import type { JSX } from "react";
import { Select, SelectTrigger, SelectValue, SelectContent, SelectItem } from "./select";
import { cn } from "../../../utils/styleutil";

interface SelectElementProps extends React.InputHTMLAttributes<HTMLSelectElement> {
    withErro: boolean;
    errorMessage?: string;
    label?: string;
    additionalClassname?: string;
    icon?: JSX.Element;
    items: SelectOption[];
    text: string;
    value?:string;
    setvalue: (field: string, value: string) => void;
}

interface SelectOption {
    value: string;
    title: string;
}

export default function SelectElement(props: SelectElementProps): JSX.Element {

    function SetValue(id?:string, value?:string){
        if(id && value){
            props.setvalue(id, value);
        }
    }

    return (
        <div className="space-y-2 text-left">
            {props.label != undefined && props.label != null && props.label.length > 0 && (
                <label className={cn("text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70")} htmlFor={props.id}>
                    {props.label}
                </label>
            )}
            <Select
                value={props.value}
                onValueChange={(value) => SetValue(props.id, value)}
                disabled={props.disabled}
            >
                <SelectTrigger className={props.withErro ? 'border-destructive' : ''}>
                    <SelectValue placeholder={props.text ?? "Selecione a posição"} />
                </SelectTrigger>
                <SelectContent>
                    {props.items.map((position) => (
                        <SelectItem key={position.value} value={position.value}>
                            {position.title}
                        </SelectItem>
                    ))}
                </SelectContent>
            </Select>
            {props.withErro && props.errorMessage && (
                <p className="text-sm text-destructive errortext">{props.errorMessage}</p>
            )}
        </div>
    );
}