import React from "react";
import type { ITableHeaderProps } from "./interfaces";
import { TableHead, TableHeaderElement, TableRow } from '../ui/table'

export function TableHeader(props: ITableHeaderProps): React.JSX.Element {
    return (
        <TableHeaderElement>
            <TableRow className='border-b transition-colors data-[state=selected]:bg-muted hover:bg-muted/50'>
                {props.Columns.map((column) => (
                    <TableHead className='h-12 px-4 text-left align-middle font-medium text-muted-foreground [&:has([role=checkbox])]:pr-0'>{column.title}</TableHead>
                ))}
            </TableRow>
        </TableHeaderElement>
    )
};