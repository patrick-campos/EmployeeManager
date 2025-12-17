type ValidationResult = {
    validatedColumns: ColumnFieldMap;
    isValid: boolean;
};


export class FieldMap {
    value:string;
    error: boolean;
    message: string;

    constructor(message: string){
        this.message = message;
        this.value = '';
        this.error = false;
    }
}

export class ColumnFieldMap {
    firstname: FieldMap;
    lastname: FieldMap;
    mail: FieldMap;
    password: FieldMap;
    position: FieldMap;
    documentType: FieldMap;
    documentNumber: FieldMap;

    constructor() {
        this.firstname = new FieldMap('First Name is required');
        this.lastname = new FieldMap('Last Name is required');
        this.mail = new FieldMap('Mail is required');
        this.password = new FieldMap('Password should be at least 12 characters long with letters, numbers and special characters');
        this.position = new FieldMap('Position is required');
        this.documentType = new FieldMap('Document Type is required');
        this.documentNumber = new FieldMap('Document Number is required');
    }

     SetErrorState(fieldName: keyof ColumnFieldMap, hasError: boolean) {
        const field = this[fieldName];
        if (field instanceof FieldMap) {
            field.error = hasError;
        }
    }

    Validate(): ValidationResult {
        
        const copy = new ColumnFieldMap();

        // clona os valores atuais
        for (const key of Object.keys(this) as (keyof ColumnFieldMap)[]) {
            const field = this[key];
            const copyField = copy[key];
            if (field instanceof FieldMap && copyField instanceof FieldMap) {
                copyField.value = field.value;
                copyField.error = false; // reset do erro
            }
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{12,}$/;

        let isValid = true;

        for (const key of Object.keys(copy) as (keyof ColumnFieldMap)[]) {
            const field = copy[key];
            if (field instanceof FieldMap) {
                // required
                if (!field.value || field.value.toString().trim() === '') {
                    field.error = true;
                }

                // validação de email
                if (key === 'mail' && field.value && !emailRegex.test(field.value.toString())) {
                    field.error = true;
                }

                // validação de senha
                if (key === 'password' && field.value && !passwordRegex.test(field.value.toString())) {
                    field.error = true;
                }

                if (field.error) isValid = false;
            }
        }

        return { validatedColumns: copy, isValid };
    }

    UpdateField(fieldName: keyof ColumnFieldMap, value: string): ColumnFieldMap {
        const copy = new ColumnFieldMap();

        // Clona valores e erros
        for (const key of Object.keys(this) as (keyof ColumnFieldMap)[]) {
            const field = this[key];
            const copyField = copy[key];
            if (field instanceof FieldMap && copyField instanceof FieldMap) {
                copyField.value = field.value;
                copyField.error = field.error;
            }
        }

        // Atualiza o campo desejado
        const fieldToUpdate = copy[fieldName];
        if (fieldToUpdate instanceof FieldMap) {
            fieldToUpdate.value = value;
        }

        return copy;
    }
}
