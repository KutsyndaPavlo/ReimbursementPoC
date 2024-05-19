export interface Program {
    id: string;
    name: string;
    description: string;
    stateId: number;
    state: string;
    startDate: Date;
    endDate: Date;
    lastModified: string;
    isCanceled: boolean;
}

export interface GetProgramsResponse {
    items: Program[];
}

export interface Page {
    limit: number;
    offset: number;
}

export interface CreateProgram {
    name: string;
    description: string;
    stateId: number;
    startDate: Date;
    endDate: Date;
}

export interface UpdateProgram {
    id: string;
    name: string;
    description: string;
    lastModified: string;
}