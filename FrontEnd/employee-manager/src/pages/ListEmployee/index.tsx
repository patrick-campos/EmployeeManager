import React, { useEffect, type JSX } from "react";
import Header from "../../components/header";
import { Table } from "../../components/table";
import type { Employee } from "../../entites/employee";
import { api } from "../../services/api";
import type { ListItemMap } from "../../entites/map/ListItemMap";
import "./style.scss";
import { useNavigate } from "react-router-dom";

export default function ListEmployee() {

    const [Employees, setEmployees] = React.useState<Array<Employee>>([]);

    const navigate = useNavigate();

    const columns: Array<ListItemMap> = [
        { title: 'Name', key: 'name', valueEmphasis: true },
        { title: 'Email', key: 'email', valueEmphasis: false },
        { title: 'Position', key: 'position', valueEmphasis: true },
        { title: '', key: 'edit', render: (item) => GetEditElement(item) },
        { title: '', key: 'delete', render: (item) => GetExcludeElement(item) }
    ];

    function GetEmployees() {
        api.get<Array<Employee>>(`/employee`).then((response: any) => {
            setEmployees(response);
        });
    }

    useEffect(() => {
        GetEmployees();
    },[]);

    function DeleteFromLocalList(id?:string){
        let employeesUpdated = [...Employees];
        employeesUpdated = employeesUpdated.filter(emp => emp.id !== id);
        setEmployees(employeesUpdated);
    }

    function DeleteItem(id?:string){
        if(!id) return;

        api.delete(`/employee/${id}`).then((response: any) => {
            DeleteFromLocalList(id);
        });
    }

    function EditItem(id?:string){
        if(!id) return;

        navigate(`/employee/${id}`);
    }

    function GetExcludeElement(empl:Employee):JSX.Element {
        return (
            <>
                <button className="text-red-600 hover:text-red-800 deleteBtn" onClick={() => DeleteItem(empl.id)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24"><line x1="4" y1="4" x2="20" y2="20" stroke="red" stroke-width="2"/><line x1="20" y1="4" x2="4" y2="20" stroke="red" stroke-width="2"/></svg>
                </button>
            </>
        )
    }

    function GetEditElement(empl:Employee):JSX.Element {
        return (
            <>
                <button className="text-red-600 hover:text-red-800 deleteBtn" onClick={() => EditItem(empl.id)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24"><path d="M3 21v-3l14-14 3 3-14 14H3z" stroke="black" stroke-width="2" fill="none"/></svg>
                </button>
            </>
        )
    }

    


    return (
        <>
            <Header title="Employee List" description='Edit or exclude employess registered' />
            <Table Columns={columns} Items={Employees} />
        </>
    )
}