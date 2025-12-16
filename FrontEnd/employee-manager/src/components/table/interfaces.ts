export interface IColumnsMapper {
    title: string;
    key: string;
    valueEmphasis: boolean;
}

export interface ITableHeaderProps {
    Columns: Array<IColumnsMapper>;
}

export interface ITableProps extends ITableHeaderProps{
    Items: Array<Record<string, any>>;
}
