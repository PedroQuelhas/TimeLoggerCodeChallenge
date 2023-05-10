import React, { useEffect, useState } from "react";
import Table from "../components/Table";
import { GetAll,AddProjectHardcoded } from "../services/projects";
import { Project } from "../services/projects/types";



export default function Projects() {
    const [projects,setProjects] = useState<Project[]>([])
    const [reload,setReload] = useState<boolean>(false)


    function HandleOnClick(){
        AddProjectHardcoded().then(()=>
        setReload(!reload))
    }

    useEffect(()=>{
        GetAll().then((data)=>setProjects(data.data));
    },[reload])

    return (
        <>
            <div className="flex items-center my-6">
                <div className="w-1/2">
                    <button onClick={()=>HandleOnClick()} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                        Add entry
                    </button>
                </div>

                <div className="w-1/2 flex justify-end">
                    <form>
                        <input
                            className="border rounded-full py-2 px-4"
                            type="search"
                            placeholder="Search"
                            aria-label="Search"
                        />
                        <button
                            className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2"
                            type="submit"
                        >
                            Search
                        </button>
                    </form>
                </div>
            </div>

           {
            projects.length>0 && <Table projects={projects} />
           }
        </>
    );
}
