/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { Id } from './Id';

export type ProjectDTO = {
    id?: Id;
    name: string;
    start_date: string;
    end_date?: string;
    deadline: string;
    completed?: boolean;
    customerId: Id;
};
