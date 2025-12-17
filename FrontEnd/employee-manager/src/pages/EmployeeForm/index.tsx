import Header from "../../components/header";
import { Button } from "../../components/ui/button/button";
import { InputTextElement } from "../../components/ui/input";
import { Save } from 'lucide-react';
import "../../components/ui/input.scss"
import SelectElement from '../../components/ui/select';
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { api, ApiError } from "../../services/api";
import type { Employee } from "../../entites/employee";
import { useNavigate } from "react-router-dom";
import { ColumnFieldMap } from "../../entites/map/FieldMap";
import Modal from "../../components/ui/modal/Modal";

export function EmployeeForm(): React.JSX.Element {
    const [form, setForm] = useState<ColumnFieldMap>(new ColumnFieldMap());

    const [isEditMode, setIsEditMode] = useState<boolean>(false);

    const { id } = useParams();
    const navigate = useNavigate();

    function InputChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.currentTarget;

        setForm((prev) => prev.UpdateField(name as keyof ColumnFieldMap, value));
    }

    function OnSelectItem(field: string, value: string) {
        setForm((prev) => prev.UpdateField(field as keyof ColumnFieldMap, value));
    }


    function GetCurrentEmployee(id: string) {
        api.get<Employee>(`/employee/${id}`).then((response: any) => {

            const form = new ColumnFieldMap();

            form.firstname.value = response.name;
            form.lastname.value = response.lastname;
            form.mail.value = response.email;
            form.password.value = 'senha@fictícia123';
            form.position.value = response.position;
            form.documentType.value = response.documentType;
            form.documentNumber.value = response.documentNumber;

            setForm(form);

        });
    }

    function BuildNewEmployeeEntity(form: ColumnFieldMap) {
        return {
            name: form.firstname.value,
            lastName: form.lastname.value,
            password: form.password.value,
            mail: form.mail.value,
            documentNumber: form.documentNumber.value,
            positionName: form.position.value,
            documentTypeName: form.documentType.value
        }
    }

    function SubmitForm(form: ColumnFieldMap) {
        let resultOfValidation = form.Validate();

        if(resultOfValidation.isValid === false){
            setForm(resultOfValidation.validatedColumns);
            return;
        }

        if (isEditMode)
            UpdateEmployee(id!, form);
        else
            SaveNewEmployee(form);
    }

    function ErrorMessageLint(message:string):string{
        if(message.includes("employee_mail_key"))
            return "It's not possible to register two employees with the same email.";
        else if(message.includes("employee_documentnumber_key"))
            return "It's not possible to register two employees with the same document number.";
        
        return message;
    }

    function SaveNewEmployee(form: any) {
        api.post<Employee>('/employee', BuildNewEmployeeEntity(form)).then((response: any) => {
            const message = response?.data?.message ?? 'Employee saved successfully.';
            setModal({ open: true, title: 'Sucesso', message, variant: 'success' });
            setTimeout(() => navigate('/'), 1200);
        }).catch((err: ApiError) => {
            const message = ErrorMessageLint(err?.body? JSON.stringify(err.body) :'Erro on save.');
            setModal({ open: true, title: 'Erro', message, variant: 'error' });
        });
    }

    function UpdateEmployee(id: string, form: any) {
        api.patch<Employee>(`/employee/${id}`, BuildNewEmployeeEntity(form)).then((response: any) => {
            const message = response?.data?.message ?? 'Employee updated successfully.';
            setModal({ open: true, title: 'Sucesso', message, variant: 'success' });
            setTimeout(() => navigate('/'), 1200);
        }).catch((err: ApiError) => {
            const message = ErrorMessageLint(err?.body? JSON.stringify(err.body) : 'Erro on update.');
            setModal({ open: true, title: 'Erro', message, variant: 'error' });
        });
    }

    type ModalState = {
        open: boolean;
        title?: string;
        message?: string;
        variant: 'success' | 'error' | 'info';
    };

    const [modal, setModal] = useState<ModalState>({ open: false, title: undefined, message: undefined, variant: 'info' });

    useEffect(() => {

        setIsEditMode(id !== undefined);

        if (id)
            GetCurrentEmployee(id);

    }, []);



    return (
        <>
            <Header title="Register Employee" description='Fill up the form below to register or update a employee' />
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <InputTextElement id="firstname" value={form.firstname.value} label="First Name" placeholder="type first name" withErro={form.firstname.error} errorMessage={form.firstname.message} change={(event) => InputChange(event)} />
                <InputTextElement id="lastname" value={form.lastname.value} label="Last Name" placeholder="type last name" withErro={form.lastname.error} errorMessage={form.lastname.message} change={(event) => InputChange(event)} />
                <InputTextElement id="mail" value={form.mail.value} label="Mail" placeholder="type mail" withErro={form.mail.error} errorMessage={form.mail.message} change={(event) => InputChange(event)} />
                <InputTextElement id="password" value={form.password.value} disabled={isEditMode} label="Password" placeholder="type password" withErro={form.password.error} errorMessage={form.password.message} type={'password'} change={(event) => InputChange(event)} />
                <SelectElement id="position" value={form.position.value} label="Position" text="select position" setvalue={OnSelectItem} withErro={form.position.error} errorMessage={form.position.message} items={[{ value: 'manager', title: 'Manager' }, { value: 'normal', title: 'Normal' }, { value: 'director', title: 'Director' }]} icon={<svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 shrink-0 opacity-50" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" /></svg>} />
                <SelectElement id="documentType" value={form.documentType.value} disabled={isEditMode} label="Document Type" text="select document type" setvalue={OnSelectItem} withErro={form.documentType.error} errorMessage={form.documentType.message} items={[{ value: 'cpf', title: 'CPF' }, { value: 'identification number', title: 'Identification Number' }]} icon={<svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 shrink-0 opacity-50" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" /></svg>} />
                <InputTextElement id="documentNumber" value={form.documentNumber.value} disabled={isEditMode} label="Document Number" placeholder="type document number" type="number" withErro={false} additionalClassname="md:col-span-2" change={(event) => InputChange(event)} />
            </div>
            <div className="flex justify-end gap-4 pt-4">
                <Button type="submit" onClick={() => SubmitForm(form)}>
                    <Save className="mr-2 h-4 w-4" />
                    {isEditMode ? 'Atualizar' : 'Cadastrar'}
                </Button>
            </div>
            <Modal open={modal.open} onClose={() => setModal(prev => ({ ...prev, open: false }))} title={modal.title} message={modal.message} variant={modal.variant} />
        </>
    );
}