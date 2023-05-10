import client  from '../common/apiClient';
import { PaginatedData } from '../common/types';
import { mapToProjects, mapToProject } from './mapper';
import { Project } from './types';

export const GetAll = async (
    offset?: number,
    limit?: number,
    filterKey?: Array<string>,
    filterValue?: Array<string>,
    // sortKey?: string,
    // sortOrder? string,
):  Promise<PaginatedData<Project>> => {
    const res = await client.getProjects({ offset, limit, filterKey, filterValue, })
    return {
      data: mapToProjects(res.data ?? []),
      pagination: {
        totalRecords: res.pagination.total_records,
        page: res.pagination.page,
        perPage: res.pagination.per_page,
      },
    }
  }


  export const AddProjectHardcoded = async():  Promise<Project> => {
    const res = await client.addProject({
      requestBody:{
        name:"test project hardcoded",
        customerId: "b62bdc98-18b1-4d75-8ac5-b3b0ca62271b",
        start_date: "10/05/2023 20:07:37 +00:00",
        end_date: "17/05/2023 20:07:37 +00:00",
        deadline: "15/05/2023 20:07:37 +00:00"
      }
    })
    return mapToProject(res)
  }