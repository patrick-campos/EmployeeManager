import React from "react";

import Header from "../../components/header";
import { Table } from "../../components/table";

export default function ListEmployee() {
    
    const columns = [
        { title: 'Name', key: 'name', valueEmphasis: true },
        { title: 'Email', key: 'email', valueEmphasis: false },
        { title: 'Position', key: 'position', valueEmphasis: true },
    ];

    const items = [
        { name: 'Patrick Campos', email: 'pcampos.desenvolvimento@gmail.com', position: 'Software Engineer' },
        { name: 'Paulo Santos', email: 'paulo.roberto@gmail.com', position: 'Staff SRE' },
    ]

    return (
        <>
            <Header title="Employee List" description='Edit or exclude employess registered' />
            <Table Columns={columns} Items={items} />
        </>
    )
}