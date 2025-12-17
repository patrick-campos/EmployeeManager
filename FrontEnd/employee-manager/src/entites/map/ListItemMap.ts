import type { JSX } from "react";
import type { Employee } from "../employee";

export interface ListItemMap {
    title: string, 
    key: string, 
    valueEmphasis?:boolean,
    render?: (employee: Employee) => JSX.Element;
}