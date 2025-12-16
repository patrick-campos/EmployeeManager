import React from "react";
import { DivWithReference, PWithReference } from "../ui/common";
import { TableHeader } from "./header";
import type { ITableProps } from "./interfaces";
import { TableElement } from "../ui/table";
import { TableBody } from "./body";

export function Table(props: ITableProps): React.JSX.Element {
    return (
        <>
            <DivWithReference className="rounded-lg border bg-card text-card-foreground shadow-sm">
                <DivWithReference className="flex flex-col space-y-1.5 p-6">
                <PWithReference className="text-sm text-muted-foreground">TESTE DE P NOVO</PWithReference>
                </DivWithReference>
                <DivWithReference className="bg-muted p-6 pt-0">
                <TableElement className='w-full caption-bottom text-sm'>
                    <TableHeader Columns={props.Columns} />
                    <TableBody Items={props.Items} Columns={props.Columns} />
                </TableElement>
                </DivWithReference>
            </DivWithReference>
        </>
    )
}