// resume-analysis.component.ts
import { Component } from '@angular/core';
import { ResumeAnalysisService, AnalysisResponse } from '../../services/resume-analysis.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpEventType } from '@angular/common/http';
enum AppState {
  UPLOAD = 'upload',
  LOADING = 'loading',
  RESULTS = 'results',
  ERROR = 'error'
}

@Component({
  selector: 'app-resume-analysis',
  templateUrl: './resume-analysis.component.html',
  styleUrls: ['./resume-analysis.component.css'], 
  imports:[CommonModule,FormsModule]
})
export class ResumeAnalysisComponent {
  // App State Management
  currentState: AppState = AppState.UPLOAD;
  readonly AppState = AppState; // Make enum available in template

  // Form Data
  resumeFile: File | null = null;
  jobDescription = '';

  // Analysis Results
  analysisResult: AnalysisResponse | null = null;

  // Loading State
  uploadProgress = 0;

  // Error Handling
  errorMessage = '';

  constructor(private resumeAnalysisService: ResumeAnalysisService) {}

  // File selection handler
  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      // Validate file type
      const allowedTypes = [
        'application/pdf',
        'application/msword',
      ];
      
      if (allowedTypes.includes(file.type)) {
        this.resumeFile = file;
        this.clearError();
      } else {
        this.showError('Please upload a PDF or Word document (.pdf, .doc, .docx)');
        this.resumeFile = null;
      }
    }
  }

  // Main analysis function
  analyzeResume(): void {
    // Validation
    if (!this.resumeFile) {
      this.showError('Please select a resume file');
      return;
    }

    if (!this.jobDescription.trim()) {
      this.showError('Please provide a job description');
      return;
    }

    if (this.jobDescription.trim().length < 50) {
      this.showError('Job description should be at least 50 characters long');
      return;
    }

    // Reset states
    this.clearError();
    this.currentState = AppState.LOADING;
    this.uploadProgress = 0;

    // API call
    this.resumeAnalysisService.analyzeResume(this.resumeFile, this.jobDescription.trim())
      .subscribe({
        next: (event) => {
          console.log('API result:', event);
          this.handleApiEvent(event);
        },
        error: (error) => {
          this.handleApiError(error);
        }
      });
  }

  // Handle API response events
 private handleApiEvent(event: any): void {
  switch (event.type) {
    case HttpEventType.UploadProgress:
      if (event.total && event.total > 0) {
        this.uploadProgress = Math.round(100 * event.loaded / event.total);
      }
      break;

    case HttpEventType.Response:
      this.analysisResult = event.body as AnalysisResponse;
      this.currentState = AppState.RESULTS;
      break;
  }
}


  // Handle API errors
  private handleApiError(error: any): void {
    console.error('Analysis error:', error);
    
    if (error.status === 0) {
      this.showError('Cannot connect to server. Please check if the API is running.');
    } else if (error.status === 413) {
      this.showError('File too large. Please upload a smaller file.');
    } else if (error.status >= 500) {
      this.showError('Server error. Please try again later.');
    } else {
      this.showError('Analysis failed. Please try again.');
    }
    
    this.currentState = AppState.ERROR;
  }

  // Reset to upload new resume
  analyzeAnother(): void {
    this.currentState = AppState.UPLOAD;
    this.resumeFile = null;
    this.jobDescription = '';
    this.analysisResult = null;
    this.uploadProgress = 0;
    this.clearError();
    
    // Reset file input
    const fileInput = document.getElementById('resumeFile') as HTMLInputElement;
    if (fileInput) fileInput.value = '';
  }

  // Error handling helpers
  private showError(message: string): void {
    this.errorMessage = message;
    this.currentState = AppState.ERROR;
  }

  private clearError(): void {
    this.errorMessage = '';
    if (this.currentState === AppState.ERROR) {
      this.currentState = AppState.UPLOAD;
    }
  }

  // Score color coding
  getScoreColor(score: any): string {
    if(!score) return '#000';
    const numScore = parseInt(score);
    if (numScore >= 80) return '#2ecc71'; // Green
    if (numScore >= 60) return '#f39c12'; // Orange
    return '#e74c3c'; // Red
  }

  // Check if form is valid for submission
  get isFormValid(): boolean {
    return !!this.resumeFile && this.jobDescription.trim().length >= 50;
  }
}