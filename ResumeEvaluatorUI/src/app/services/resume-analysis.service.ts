import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface AnalysisResponse {
  overallScore: string;
  fitStatus: string;
  experienceMatch: string;
  skillMatch: string;
  missingSkills: string[];
  extraSkills: string[];
  strengths: string[];
  improvementAreas: string[];
  suggestions: string[];
  resumeSummary: string;
}

@Injectable({
  providedIn: 'root'
})
export class ResumeAnalysisService {
  private apiUrl = 'https://localhost:7034/api/resume/evaluate';

  constructor(private http: HttpClient) { }

  analyzeResume(resumeFile: File, jobDescription: string): Observable<HttpEvent<AnalysisResponse>> {
    const formData = new FormData();
    formData.append('ResumeFile', resumeFile);
    formData.append('JobDescription', jobDescription);

    return this.http.post<AnalysisResponse>(this.apiUrl, formData, {
      reportProgress: true,
      observe: 'events'
    });
  }
}
