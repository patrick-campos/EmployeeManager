import type { Employee } from "../../entites/employee";
import type { ListItemMap } from "../../entites/map/ListItemMap";

export interface IColumnsMapper {
    title: string;
    key: string;
    valueEmphasis: boolean;
}

export interface ITableHeaderProps {
    Columns: Array<ListItemMap>;
}

export interface ITableProps extends ITableHeaderProps{
    Items: Array<Employee>;
}
