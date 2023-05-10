/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CustomerDTO } from '../models/CustomerDTO';
import type { Id } from '../models/Id';
import type { PaginationDTO } from '../models/PaginationDTO';
import type { ProjectDTO } from '../models/ProjectDTO';
import type { ProjectReportDTO } from '../models/ProjectReportDTO';
import type { TimeslotDTO } from '../models/TimeslotDTO';

import type { CancelablePromise } from '../core/CancelablePromise';
import type { BaseHttpRequest } from '../core/BaseHttpRequest';

export class DefaultService {

    constructor(public readonly httpRequest: BaseHttpRequest) {}

    /**
     * Create a customer
     * @returns CustomerDTO The newly created customer
     * @throws ApiError
     */
    public addCustomer({
requestBody,
}: {
/**
 * Customer payload
 */
requestBody?: CustomerDTO,
}): CancelablePromise<CustomerDTO> {
        return this.httpRequest.request({
            method: 'POST',
            url: '/customer',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Fetch a list of customers
     * @returns any A list of customers
     * @throws ApiError
     */
    public getCustomers({
offset,
limit,
filterKey,
filterValue,
sortKey,
sortOrder,
}: {
/**
 * The number of items to skip before starting to collect the result set
 */
offset?: number,
/**
 * The numbers of items to return
 */
limit?: number,
/**
 * name of the field to filter by
 */
filterKey?: Array<string>,
filterValue?: Array<string>,
/**
 * value of the field to sort by
 */
sortKey?: string,
/**
 * sort order
 */
sortOrder?: 'asc' | 'desc',
}): CancelablePromise<{
data: Array<CustomerDTO>;
pagination: PaginationDTO;
}> {
        return this.httpRequest.request({
            method: 'GET',
            url: '/customer',
            query: {
                'offset': offset,
                'limit': limit,
                'filter key': filterKey,
                'filter value': filterValue,
                'sort-key': sortKey,
                'sort-order': sortOrder,
            },
            errors: {
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Fetch a customer
     * @returns CustomerDTO A customer
     * @throws ApiError
     */
    public getCustomer({
id,
}: {
/**
 * The unique identifier
 */
id: Id,
}): CancelablePromise<CustomerDTO> {
        return this.httpRequest.request({
            method: 'GET',
            url: '/customer/{id}',
            path: {
                'id': id,
            },
            errors: {
                404: `No customer found for the provided \`Id\``,
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Patch a customer
     * Body schema should follow format RFC 6902 (application/json-patch+json)
     * @returns any Success patching a customer
     * @throws ApiError
     */
    public updateCustomer({
id,
requestBody,
}: {
/**
 * The unique identifier
 */
id: Id,
requestBody?: Record<string, any> | null,
}): CancelablePromise<any> {
        return this.httpRequest.request({
            method: 'PATCH',
            url: '/customer/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                404: `No customer found for the provided \`Id\``,
                409: `Conflict detected during update`,
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Delete a customer
     * @returns any Success deleting a customer
     * @throws ApiError
     */
    public deleteCustomer({
id,
}: {
/**
 * The unique identifier
 */
id: Id,
}): CancelablePromise<any> {
        return this.httpRequest.request({
            method: 'DELETE',
            url: '/customer/{id}',
            path: {
                'id': id,
            },
            errors: {
                404: `No customer found for the provided \`Id\``,
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Create a project
     * @returns ProjectDTO The newly created project
     * @throws ApiError
     */
    public addProject({
requestBody,
}: {
/**
 * Project payload
 */
requestBody?: ProjectDTO,
}): CancelablePromise<ProjectDTO> {
        return this.httpRequest.request({
            method: 'POST',
            url: '/projects',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Fetch a list of projects
     * @returns any A list of projects
     * @throws ApiError
     */
    public getProjects({
offset,
limit,
filterKey,
filterValue,
sortKey,
sortOrder,
}: {
/**
 * The number of items to skip before starting to collect the result set
 */
offset?: number,
/**
 * The numbers of items to return
 */
limit?: number,
/**
 * name of the field to filter by
 */
filterKey?: Array<string>,
/**
 * value of the field to filter with
 */
filterValue?: Array<string>,
/**
 * value of the field to sort by
 */
sortKey?: string,
/**
 * sort order
 */
sortOrder?: 'asc' | 'desc',
}): CancelablePromise<{
data: Array<ProjectDTO>;
pagination: PaginationDTO;
}> {
        return this.httpRequest.request({
            method: 'GET',
            url: '/projects',
            query: {
                'offset': offset,
                'limit': limit,
                'filter key': filterKey,
                'filter value': filterValue,
                'sort-key': sortKey,
                'sort-order': sortOrder,
            },
            errors: {
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Fetch a list of projects
     * @returns any A list of project overview
     * @throws ApiError
     */
    public getProjectsOverview({
offset,
limit,
filterKey,
filterValue,
sortKey,
sortOrder,
}: {
/**
 * The number of items to skip before starting to collect the result set
 */
offset?: number,
/**
 * The numbers of items to return
 */
limit?: number,
/**
 * name of the field to filter by
 */
filterKey?: Array<string>,
/**
 * value of the field to filter with
 */
filterValue?: Array<string>,
/**
 * value of the field to sort by
 */
sortKey?: string,
/**
 * sort order
 */
sortOrder?: 'asc' | 'desc',
}): CancelablePromise<{
data: Array<ProjectReportDTO>;
pagination: PaginationDTO;
}> {
        return this.httpRequest.request({
            method: 'GET',
            url: '/projects/overview',
            query: {
                'offset': offset,
                'limit': limit,
                'filter key': filterKey,
                'filter value': filterValue,
                'sort-key': sortKey,
                'sort-order': sortOrder,
            },
            errors: {
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Search list of projects
     * @returns ProjectDTO A list of projects
     * @throws ApiError
     */
    public searchProjects({
offset,
limit,
searchKey,
searchValue,
filterKey,
filterValue,
sortKey,
sortOrder,
}: {
/**
 * The number of items to skip before starting to collect the result set
 */
offset: number,
/**
 * The numbers of items to return
 */
limit: number,
/**
 * name of the field to search by
 */
searchKey: Array<string>,
/**
 * value of the field to search with
 */
searchValue: Array<string>,
/**
 * name of the field to filter by
 */
filterKey?: Array<string>,
/**
 * value of the field to filter with
 */
filterValue?: Array<string>,
/**
 * value of the field to sort by
 */
sortKey?: string,
/**
 * sort order
 */
sortOrder?: 'asc' | 'desc',
}): CancelablePromise<Array<ProjectDTO>> {
        return this.httpRequest.request({
            method: 'GET',
            url: '/projects/search',
            query: {
                'offset': offset,
                'limit': limit,
                'search-key': searchKey,
                'search-value': searchValue,
                'filter key': filterKey,
                'filter value': filterValue,
                'sort-key': sortKey,
                'sort-order': sortOrder,
            },
            errors: {
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Fetch a project
     * @returns ProjectDTO A project
     * @throws ApiError
     */
    public getProject({
id,
}: {
/**
 * The unique identifier
 */
id: Id,
}): CancelablePromise<ProjectDTO> {
        return this.httpRequest.request({
            method: 'GET',
            url: '/projects/{id}',
            path: {
                'id': id,
            },
            errors: {
                404: `No project found for the provided \`Id\``,
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Patch a project
     * Body schema should follow format RFC 6902 (application/json-patch+json)
     * @returns any Success patching a project
     * @throws ApiError
     */
    public updateProject({
id,
requestBody,
}: {
/**
 * The unique identifier
 */
id: Id,
requestBody?: Record<string, any> | null,
}): CancelablePromise<any> {
        return this.httpRequest.request({
            method: 'PATCH',
            url: '/projects/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                404: `No project found for the provided \`Id\``,
                409: `Conflict detected during update`,
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Delete a project
     * @returns any Success deleting a project
     * @throws ApiError
     */
    public deleteProject({
id,
}: {
/**
 * The unique identifier
 */
id: Id,
}): CancelablePromise<any> {
        return this.httpRequest.request({
            method: 'DELETE',
            url: '/projects/{id}',
            path: {
                'id': id,
            },
            errors: {
                404: `No project found for the provided \`Id\``,
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Create a Timeslot
     * @returns TimeslotDTO The newly created Timeslot
     * @throws ApiError
     */
    public addProjectTimeslot({
id,
requestBody,
}: {
/**
 * The unique identifier
 */
id: Id,
/**
 * Timeslot payload
 */
requestBody?: TimeslotDTO,
}): CancelablePromise<TimeslotDTO> {
        return this.httpRequest.request({
            method: 'POST',
            url: '/projects/{id}/timeslots',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                500: `Unexpected error`,
            },
        });
    }

    /**
     * Fetch a list of timeslots
     * @returns any A list of timeslots
     * @throws ApiError
     */
    public getProjectTimeslots({
id,
offset,
limit,
filterKey,
filterValue,
sortKey,
sortOrder,
}: {
/**
 * The unique identifier
 */
id: Id,
/**
 * The number of timeslots to skip before starting to collect the result set
 */
offset?: number,
/**
 * The numbers of timeslots to return
 */
limit?: number,
/**
 * name of the field to filter by
 */
filterKey?: Array<string>,
/**
 * value of the field to filter with
 */
filterValue?: Array<string>,
/**
 * value of the field to sort by
 */
sortKey?: string,
/**
 * sort order
 */
sortOrder?: 'asc' | 'desc',
}): CancelablePromise<{
data: Array<TimeslotDTO>;
pagination: PaginationDTO;
}> {
        return this.httpRequest.request({
            method: 'GET',
            url: '/projects/{id}/timeslots',
            path: {
                'id': id,
            },
            query: {
                'offset': offset,
                'limit': limit,
                'filter key': filterKey,
                'filter value': filterValue,
                'sort-key': sortKey,
                'sort-order': sortOrder,
            },
            errors: {
                500: `Unexpected error`,
            },
        });
    }

}
