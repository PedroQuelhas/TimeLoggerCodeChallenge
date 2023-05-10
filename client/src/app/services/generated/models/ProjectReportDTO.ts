/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { Id } from './Id';

export type ProjectReportDTO = {
    project_id: Id;
    project_name: string;
    start_date: string;
    end_date?: string;
    deadline?: string;
    completed?: boolean;
    total_time: number;
    total_records: number;
    customer_name?: string;
};
