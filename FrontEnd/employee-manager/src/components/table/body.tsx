import React from "react";
import type { ITableProps } from "./interfaces";
import { TableBodyElement, TableCell, TableRow } from '../ui/table'

export function TableBody(props: ITableProps): React.JSX.Element {
    return (
        <TableBodyElement className='[&_tr:last-child]:border-0'>
            {props.Items.map((item, index) => (
                <TableRow className='animate-fade-in border-b transition-colors data-[state=selected]:bg-muted hover:bg-muted/50' style={{ animationDelay: `${1 * 50}ms` }}>
                    {props.Columns.map((column) => (
                        <TableCell className={`p-4 align-middle [&:has([role=checkbox])]:pr-0 ${column.valueEmphasis ? 'font-medium' : 'text-muted-foreground'}`}>
                            {item[column.key]}
                        </TableCell>
                    ))}
                </TableRow>
            ))}

        </TableBodyElement>
    )
};