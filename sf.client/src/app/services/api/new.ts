///// For Loging in \\\\\

export class LoginFormModel {
    username?: string;
    password?: string;
    success?: boolean;
    error?: boolean;
    message?: string;
    validationMessages?: { [key: string]: string };

    constructor(data?: Partial<LoginFormModel>) {
        if (data) {
            this.init(data);
        }
    }

    init(data: Partial<LoginFormModel>) {
        if (data) {
            this.username = data.username;
            this.password = data.password;
            this.success = data.success;
            this.error = data.error;
            this.message = data.message;
            this.validationMessages = data.validationMessages;
        }
    }

    static fromJS(data: any): LoginFormModel {
        let result = new LoginFormModel();
        result.init(data);
        return result;
    }

    toJSON(): any {
        return {
            username: this.username,
            password: this.password,
            success: this.success,
            error: this.error,
            message: this.message,
            validationMessages: this.validationMessages
        };
    }

    getValidationErrors(): string[] {
        const errors = [];
        if (!this.username) {
            errors.push('Username is required');
        }
        if (!this.password) {
            errors.push('Password is required');
        }
        return errors;
    }
}


////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\