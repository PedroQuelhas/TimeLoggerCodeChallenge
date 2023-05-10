import moment from "moment";
import { ProjectDTO } from "../../services/generated";
import { Project } from "./types";

export const mapToProject = (data: ProjectDTO): Project => {
    return {
        id:data.id ?? '',
        completed: data.completed ?? false,
        customerId: data.customerId,
        deadline: data.deadline === undefined ? undefined : moment(data.deadline,"DDMMYYYY").toDate(),
        end_date:data.end_date === undefined ? undefined : moment(data.end_date,"DDMMYYYY").toDate(),
        start_date:data.start_date === undefined ? undefined : moment(data.start_date,"DDMMYYYY").toDate(),
        name: data.name
    }
}  

export const mapToProjects = (data: ProjectDTO[]): Project[] => {
    let list: Project[] = []
  
    data.forEach((element) => {
      list.push(mapToProject(element))
    })
    return list
  }